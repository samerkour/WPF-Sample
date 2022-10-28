using Hs.Domain.Entities.SampleDbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Presentation.ViewModel
{
    public class CarItemViewModel: ValidationViewModelBase
    {

        private readonly Car? _model;

        public CarItemViewModel(Car? model)
        {
            _model = model;
        }

        public string? Name {

            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
                if (string.IsNullOrEmpty(_model.Name))
                {
                    AddError("Car Name is required");
                }
                else
                {
                    ClearErrors();
                }
            }
        }
        public string? Model {
            get => _model.Model;
            set
            {
                _model.Model = value;
                RaisePropertyChanged();
            }
        }
        public short? Year
        {
            get => _model.Year;
            set
            {
                _model.Year = value;
                RaisePropertyChanged();
            }
        }
        public bool IsDeleted {
            get => _model.IsDeleted;
            set
            {
                _model.IsDeleted = value;
                RaisePropertyChanged();
            }
        }
        public bool IsEnabled {
            get => _model.IsEnabled;
            set
            {
                _model.IsEnabled = value;
                RaisePropertyChanged();
            }
        }
        public DateTime CreationDateTime
        {
            get => _model.CreationDateTime;
            set
            {
                _model.CreationDateTime = value;
                RaisePropertyChanged();
            }
        }
        public DateTime UpdateDateTime {
            get => _model.UpdateDateTime;
            set
            {
                _model.UpdateDateTime = value;
                RaisePropertyChanged();
            }
        }
        public DateTime? DeleteDateTime {
            get => _model.DeleteDateTime;
            set
            {
                _model.DeleteDateTime = value;
                RaisePropertyChanged();
            }
        }
    }
}
