using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
using FluentValidation;

namespace GulRuby.Business.Entities
{
    public class Airline : ObjectBase, IIdentifiableEntity
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(() => ID);
                }
            }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }


        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                if (_code != value)
                {
                    _code = value;
                    OnPropertyChanged(() => Code);
                }
            }
        }

        private string _addedBy;
        public string AddedBy
        {
            get { return _addedBy; }
            set
            {
                if (_addedBy != value)
                {
                    _addedBy = value;
                    OnPropertyChanged(() => AddedBy);
                }
            }
        }


        private DateTime _addedTime;
        public DateTime AddedTime
        {
            get { return _addedTime; }
            set
            {
                if (_addedTime != value)
                {
                    _addedTime = value;
                    OnPropertyChanged(() => AddedTime);
                }
            }
        }


        [NotNavigable]
        public int EntityId
        {
            get { return ID; }
            set { ID = value; }
        }

        class AirlineValidator : AbstractValidator<Airline>
        {
            public AirlineValidator()
            {
                RuleFor(obj => obj.Name).NotEmpty();
                RuleFor(obj => obj.Code).NotEmpty();
                RuleFor(obj => obj.AddedBy).NotEmpty();
               // TODO: Add Unique check validator RuleFor(obj => obj.Code);
            }
        }

        protected override IValidator GetValidator()
        {
            return new AirlineValidator();
        }

    }
}
