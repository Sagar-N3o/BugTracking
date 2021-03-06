﻿using System.Collections.Generic;

namespace BugTracking.Business.ViewModels
{
    public class Bug_StatusViewModel
    {
        public Bug_StatusViewModel()
        {
            BugViewModels = new HashSet<BugViewModel>();
        }

        public int Id { get; set; }
        public string BugStatus { get; set; }

        public virtual ICollection<BugViewModel> BugViewModels { get; set; }
    }
}
