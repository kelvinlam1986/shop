namespace Shop.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbFactory _dbFactory;
        private ShopDbContext _dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public ShopDbContext DbContext
        {
            get { return this._dbContext ?? this._dbFactory.Init(); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
