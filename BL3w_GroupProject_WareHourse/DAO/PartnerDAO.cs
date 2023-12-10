﻿using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class PartnerDAO
    {
        private static PartnerDAO instance = null;

        public PartnerDAO() { }

        public static PartnerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PartnerDAO();
                }
                return instance;
            }
        }

        public List<Partner> GetPartners()
        {
            List<Partner> partner;
            try
            {
                var context = new PRN221_Fall23_3W_WareHouseManagementContext();
                partner = context.Partners
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return partner;
        }

        public Partner GetPartnerByID(int id)
        {
            Partner partner = null;
            try
            {
                var db = new PRN221_Fall23_3W_WareHouseManagementContext();
                partner = db.Partners.SingleOrDefault(u => u.PartnerId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return partner;
        }

        public void AddPartner(Partner partner)
        {
            try
            {
                bool existingPartner = GetPartners()
                    .Any(a => a.PartnerCode.ToLower().Equals(partner.PartnerCode.ToLower()));

                if (existingPartner == true)
                {
                    partner.Status = 1;

                    using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                    {
                        db.Partners.Add(partner);
                        db.SaveChanges();
                    }
                }
                else
                {
                    throw new Exception("Partner already exists!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in Add Partner: {ex.Message}", ex);
            }
        }

        public bool UpdatePartner(Partner partner)
        {
            try
            {
                using (var db = new PRN221_Fall23_3W_WareHouseManagementContext())
                {
                    var existing = db.Partners.SingleOrDefault(x => x.PartnerId == partner.PartnerId);
                    if (existing != null)
                    {
                        existing.PartnerCode = partner.PartnerCode;
                        existing.Name = partner.Name;
                        existing.Status = partner.Status;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Partner not found for updating.");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePartner: {ex.Message}", ex);
                return false;
            }
        }

        public void BanPartner(int id)
        {
            try
            {
                Partner u = GetPartnerByID(id);
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
                    throw new Exception("Partner does not exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }   
}