using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class StorageAreaDAO
    {
        private static StorageAreaDAO instance = null;

        public StorageAreaDAO() { }

        public static StorageAreaDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StorageAreaDAO();
                }
                return instance;
            }
        }

        public List<StorageArea> GetStorageAreas()
        {
            List<StorageArea> storageArea;
            try
            {
                var context = new PRN221_Fall23_3W_WareHouseManagementContext();
                storageArea = context.StorageAreas
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return storageArea;
        }

        public StorageArea GetStorageAreaByID(int id)
        {
            StorageArea storage = null;
            try
            {
                var db = new PRN221_Fall23_3W_WareHouseManagementContext();
                storage = db.StorageAreas.SingleOrDefault(u => u.AreaId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return storage;
        }

        public bool AddStorageArea(StorageArea storage)
        {
            try
            {
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    bool existingStorageArea = db.StorageAreas
                        .Any(a => a.AreaCode.ToLower() == storage.AreaCode.ToLower());

                    if (!existingStorageArea)
                    {
                        storage.Status = 1;
                        db.StorageAreas.Add(storage);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("StorageArea already exists!");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add StorageArea: {ex.Message}", ex);
                return false;
            }
        }

        public bool UpdateStorageArea(StorageArea storage)
        {
            try
            {
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    var existing = db.StorageAreas.SingleOrDefault(x => x.AreaId == storage.AreaId);
                    if (existing != null)
                    {
                        existing.AreaCode = storage.AreaCode;
                        existing.AreaName = storage.AreaName;
                        existing.Status = storage.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("StorageArea not found for updating.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateStorageArea: {ex.Message}", ex);
                return false;
            }
        }
    }
}
