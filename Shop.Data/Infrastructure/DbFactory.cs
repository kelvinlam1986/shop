using System;

namespace Shop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ShopDbContext dbContext;

        public ShopDbContext Init()
        {
            return dbContext ?? (dbContext = new ShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
