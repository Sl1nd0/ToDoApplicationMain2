namespace Services.Core.Interfaces
{
    public interface IQuery<TReq, TRes> 
    {
       Task<TRes> Query(TReq model);
    }
}
