
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Data;

namespace SpaceInvaders.Utils;

/// <summary>
/// An observable collection that supports incremental data loading.
/// Ideal for scenarios where data needs to be loaded in parts (pagination)
/// to improve performance and user experience.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
public class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
{
    private readonly Func<int, Task<List<T>>> _fetchPage;
    private readonly DispatcherQueue _dispatcher;
    private bool _hasMoreItems = true;
    private bool _isLoading = false;
    private int _currentPage = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementalLoadingCollection{T}"/> class.
    /// </summary>
    /// <param name="fetchPage">An asynchronous function that takes the page number
    /// and returns a list of items for that page.</param>
    /// <param name="dispatcher">The <see cref="DispatcherQueue"/> to enqueue operations on the UI thread.</param>
    public IncrementalLoadingCollection(Func<int, Task<List<T>>> fetchPage, DispatcherQueue dispatcher)
    {
        _fetchPage = fetchPage;
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IncrementalLoadingCollection{T}"/> class with initial items.
    /// </summary>
    /// <param name="initialItems">The initial collection of items.</param>
    /// <param name="fetchPage">An asynchronous function that takes the page number
    /// and returns a list of items for that page.</param>
    /// <param name="dispatcher">The <see cref="DispatcherQueue"/> to enqueue operations on the UI thread.</param>
    public IncrementalLoadingCollection(IEnumerable<T> initialItems, Func<int, Task<List<T>>> fetchPage, DispatcherQueue dispatcher) : base(initialItems)
    {
        _fetchPage = fetchPage;
        _dispatcher = dispatcher;
        _currentPage = 2; // We already have page 1
    }

    /// <summary>
    /// Gets a value indicating whether there are more items to load.
    /// </summary>
    public bool HasMoreItems => _hasMoreItems;

    /// <summary>
    /// Asynchronously loads more items into the collection.
    /// </summary>
    /// <param name="count">The number of items to load (suggestion, not a strict limit).</param>
    /// <returns>An asynchronous operation that returns the result of loading more items.</returns>
    public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
    {
        if (_isLoading)
        {
            return Task.FromResult(new LoadMoreItemsResult { Count = 0 }).AsAsyncOperation();
        }

        _isLoading = true;

        return Task.Run(async () =>
        {
            try
            {
                var newItems = await _fetchPage(_currentPage);

                if (newItems != null && newItems.Any())
                {
                    _currentPage++;
                    _dispatcher.TryEnqueue(() =>
                    {
                        foreach (var item in newItems)
                        {
                            Add(item);
                        }
                    });
                    
                    return new LoadMoreItemsResult { Count = (uint)newItems.Count };
                }
                else
                {
                    _hasMoreItems = false;
                    return new LoadMoreItemsResult { Count = 0 };
                }
            }
            finally
            {
                _isLoading = false;
            }
        }).AsAsyncOperation();
    }
}
