#region Usings

using OrderPicking.Models;

#endregion

namespace OrderPicking.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Constructors

        public ItemDetailViewModel(Item item = null)
        {
            this.Title = item?.Text;
            this.Item = item;
        }

        #endregion

        #region Properties

        public Item Item { get; set; }

        #endregion
    }
}