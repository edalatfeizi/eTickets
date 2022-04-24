namespace eTickets.Data.Base
{
    public interface IEntityBase
    {
        //if the child classes have the Id property it will be override by this Id property
        //also you can remove Id properties in child classes
        int Id { get; set; } 

    }
}
