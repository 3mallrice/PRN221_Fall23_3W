using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;

        public AccountDAO() { }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public List<Account> GetAccounts()
        {
            List<Account> account;
            try
            {
                var context = new PRN221_Fall23_3W_WareHouseManagementContext();
                account = context.Accounts
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public Account GetAccountByID(int id)
        {
            Account account = null;
            try
            {
                var db = new PRN221_Fall23_3W_WareHouseManagementContext();
                account = db.Accounts.SingleOrDefault(u => u.AccountId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return account;
        }

        public void AddAccount(Account account)
        {
            try
            {
                bool existingAccount = GetAccounts()
                    .Any(a => a.AccountCode.ToLower().Equals(account.AccountCode.ToLower()));
                
                if (existingAccount == true)
                {
                    account.Status = 1;

                    using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                    {
                        db.Accounts.Add(account);
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Account already exists!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Add Account: {ex.Message}", ex);
            }
        }

        public bool UpdateAccount(Account account)
        {
            try
            {
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    var existing = db.Accounts.SingleOrDefault(x => x.AccountId == account.AccountId);
                    if (existing != null)
                    {
                        existing.AccountCode = account.AccountCode;
                        existing.Name = account.Name;
                        existing.Email = account.Email;
                        existing.Phone = account.Phone;
                        existing.Password = account.Password;
                        existing.Status = account.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Account not found for updating.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateAccount: {ex.Message}", ex);
                return false;
            }
        }

        public void BanAccount(int id)
        {
            try
            {
                Account u = GetAccountByID(id);
                if (u != null)
                {
                    if (u.Status == 0)
                    {
                        u.Status = 1;
                    }
                    else
                    {
                        u.Status = 0;
                    }
                    using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                    {
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Account does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
