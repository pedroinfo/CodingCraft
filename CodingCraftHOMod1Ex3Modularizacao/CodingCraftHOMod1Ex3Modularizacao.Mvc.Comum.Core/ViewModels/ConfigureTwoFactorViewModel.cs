using System.Collections.Generic;

namespace CodingCraftHOMod1Ex3Modularizacao.ViewModels
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}