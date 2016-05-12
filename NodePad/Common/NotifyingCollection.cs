using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;

namespace NodePad.Common
{
    public interface IReportsChanges<out T>
    {
        IObservable<T> ReportAChange { get; }
        string Key { get; }
    }

    public class NotifyingCollection<T> : ObservableCollection<T> where T : IReportsChanges<T>
    {

        private readonly Subject<T> _memberChanged = new Subject<T>();
        public IObservable<T> OnMemberChanged => _memberChanged;

        private Dictionary<string, IDisposable> _subscriptions = new Dictionary<string, IDisposable>() ;

        protected override void InsertItem(int index, T item)
        {
            if (_subscriptions.ContainsKey(item.Key))
            {
                throw new Exception($"key:{item.Key} already in dictionary");
            }
            var subscr = item.ReportAChange.Subscribe(t=> _memberChanged.OnNext(t));
            _subscriptions[item.Key] = subscr;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            _subscriptions[this[index].Key].Dispose();
            _subscriptions.Remove(this[index].Key);
            base.RemoveItem(index);
        }
    }

}
