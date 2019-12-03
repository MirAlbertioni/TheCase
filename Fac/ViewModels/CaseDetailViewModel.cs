using System;

using Fac.Models;

namespace Fac.ViewModels
{
    public class CaseDetailViewModel : BaseViewModel
    {
        public CaseSummary Case { get; set; }

        public CaseDetailViewModel(CaseSummary item)
        {
            Title = item?.CategoryName;
            Case = item;
        }
    }
}
