using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Aspectize.Core;
using AdventureWorks.Production;
using AdventureWorks.HumanResources;
using AdventureWorks.Person;
using AdventureWorks.Sales;

namespace PlayGroundRT
{
    public class ServiceName
    {
        public const string ADWDB = "ADWDB";
    }

    public interface ILoadDataService
    {
        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadCategories();

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadCategoriesAndSubCategories();

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadSubcategories(int categoryID);

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadProducts(int subcategoryID);

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadAllProducts();

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadAllCategoriesAndProducts();

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        Byte[] LoadImage(int productId);

        [Command(BrowserCacheDuration = "30 Days", ServerCacheDuration = "30 days")]
        DataSet LoadSalesOrdersHeader(int salesPersonId);

    }

    [Service(Name = "LoadDataService")]
    public class LoadDataService : ILoadDataService //, IInitializable, ISingleton
    {

        DataSet ILoadDataService.LoadCategories()
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            dm.LoadEntities<Category>();

            return dm.Data;
        }

        DataSet ILoadDataService.LoadCategoriesAndSubCategories()
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            var relations = new List<IRoleRelationQuery>();

            relations.Add(new RoleRelationQuery<Category, CategorySubcategory>());

            dm.LoadEntitiesGraph<Category>(relations);

            return dm.Data;
        }


        DataSet ILoadDataService.LoadSubcategories(int categoryID)
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            dm.LoadAssociated<Subcategory, CategorySubcategory>(categoryID);

            return dm.Data;
        }

        DataSet ILoadDataService.LoadProducts(int subcategoryID)
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            dm.LoadDatabaseEnum<UnitMeasure>();

            var relations = new List<IRoleRelationQuery>();
            
            relations.Add(new RoleRelationQuery<Subcategory, ProductSubcategory>());
            relations.Add(new RoleRelationQuery<Product, ProductProductPhoto>());
            
            dm.LoadEntitiesGraph<Subcategory>(relations, subcategoryID);

            return dm.Data;
        }

        DataSet ILoadDataService.LoadAllProducts()
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            dm.LoadEntities<Product>();

            return dm.Data;
        }




        DataSet ILoadDataService.LoadAllCategoriesAndProducts()
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            dm.LoadDatabaseEnum<UnitMeasure>();

            var relations = new List<IRoleRelationQuery>();

            relations.Add(new RoleRelationQuery<Category, CategorySubcategory>());
            relations.Add(new RoleRelationQuery<Subcategory, ProductSubcategory>());
            relations.Add(new RoleRelationQuery<Product, ProductProductPhoto>());

            dm.LoadEntitiesGraph<Category>(relations);

            return dm.Data;
        }


        byte[] ILoadDataService.LoadImage(int productId)
        {
            IDataManager dataManager = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            List<ProductPhoto> productPhotos = dataManager.GetAssociated<ProductPhoto, ProductProductPhoto>(productId);

            if (productPhotos.Count > 0)
            {
                return productPhotos[0].LargePhoto ?? EmptyImage.Bytes;
            }

            return EmptyImage.Bytes;
        }

        DataSet ILoadDataService.LoadSalesOrdersHeader(int salesPersonId)
        {
            IDataManager dm = EntityManager.FromDataBaseService(ServiceName.ADWDB);

            var relations = new List<IRoleRelationQuery>();

            relations.Add(new RoleRelationQuery<SalesPerson, SalesOrderHeaderSalesPerson>());
            relations.Add(new RoleRelationQuery<SalesPerson, ContactSalesPerson>());
            relations.Add(new RoleRelationQuery<SalesOrderHeader, SalesOrderHeaderSalesReason>());

            dm.LoadEntitiesGraph<SalesPerson>(relations, salesPersonId);

            return dm.Data;
        }


    }

}


