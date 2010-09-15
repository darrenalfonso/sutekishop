using FluentNHibernate.Mapping;

namespace Suteki.Shop.Maps
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id);

            Map(x => x.Email);
            Map(x => x.AdditionalInformation);
            Map(x => x.UseCardHolderContact);
            Map(x => x.PayByTelephone);
            Map(x => x.CreatedDate);
            Map(x => x.DispatchedDate);
            Map(x => x.Note);
            Map(x => x.ContactMe);

            References(x => x.Card).Cascade.All();
            References(x => x.Basket);
            References(x => x.CardContact).Cascade.All();
            References(x => x.DeliveryContact).Cascade.All();
            References(x => x.OrderStatus);
            References(x => x.User).Cascade.All();
        }
    }
}