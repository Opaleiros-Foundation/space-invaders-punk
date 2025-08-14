
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Data;

namespace SpaceInvaders.Utils;

public class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
{
    private readonly Func<int, Task<List<T>>> _fetchPage;
    private readonly DispatcherQueue _dispatcher;
    private bool _hasMoreItems = true;
    private bool _isLoading = false;
    private int _currentPage = 1;

    public IncrementalLoadingCollection(Func<int, Task<List<T>>> fetchPage, DispatcherQueue dispatcher)
    {
        _fetchPage = fetchPage;
        _dispatcher = dispatcher;
    }

    public IncrementalLoadingCollection(IEnumerable<T> initialItems, Func<int, Task<List<T>>> fetchPage, DispatcherQueue dispatcher) : base(initialItems)
    {
        _fetchPage = fetchPage;
        _dispatcher = dispatcher;
        _currentPage = 2; // We already have page 1
    }

    public bool HasMoreItems => _hasMoreItems;

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
