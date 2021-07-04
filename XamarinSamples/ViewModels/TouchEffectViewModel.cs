using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using XamarinSamples.Models;

namespace XamarinSamples.ViewModels
{
    public class TouchEffectViewModel : BindableBase
    {
        public TouchEffectViewModel()
        {
            InitializeCollection();
            _clickCommand = new DelegateCommand<TouchEffectModel>(OnClicked);
            _longClickCommand = new DelegateCommand<TouchEffectModel>(OnLongClicked);            
        }

        private ICommand _clickCommand;

        public ICommand ClickCommand
        {
            get => _clickCommand;
            set
            {
                _clickCommand = value;
                RaisePropertyChanged();
            }
        }

        private ICommand _longClickCommand;

        public ICommand LongClickCommand
        {
            get => _longClickCommand;
            set
            {
                _longClickCommand = value;
                RaisePropertyChanged();
            }
        }

        private void OnClicked(TouchEffectModel param)
        {
        }

        private void OnLongClicked(TouchEffectModel param)
        {
        }

        private ICollection<TouchEffectModel> _collection;
        public ICollection<TouchEffectModel> Collection
        {
            get => _collection;
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        void InitializeCollection()
        {
            if (_collection is null) _collection = new List<TouchEffectModel>();
            _collection.Add(new TouchEffectModel("I'm item 1"));
            _collection.Add(new TouchEffectModel("I'm item 2"));
            _collection.Add(new TouchEffectModel("I'm item 3"));
            _collection.Add(new TouchEffectModel("I'm item 4"));
            _collection.Add(new TouchEffectModel("I'm item 5"));
        }
    }
}