using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using veri;

namespace burak.Models
{
    public static class Data
    {
        public static veri.Menuler[] menumi
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Menulers.OrderByDescending(p => p.MenuTarih).Where(p => p.MenuAktif == "true" & p.MenuMi == "true" & p.KategoriMi == "false").ToArray();
                }
            }
        }

        public static veri.Menuler[] kategorimi
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Menulers.Include(p => p.Kategorilers).OrderByDescending(p => p.MenuTarih).Where(p => p.MenuAktif == "true" & p.MenuMi == "false"
                    & p.KategoriMi == "true").ToArray();
                }
            }
        }

        public static veri.Menuler[] kategorimi1
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Menulers.Include(p => p.Kategorilers).OrderBy(p => p.MenuTarih).Where(p => p.MenuAktif == "true" & p.MenuMi == "false"
                    & p.KategoriMi == "true").ToArray();
                }
            }
        }

        public static veri.SiteAyar[] SiteAyar
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.SiteAyars.Include(p => p.Kullanicilar).ToArray();
                }
            }
        }

        public static veri.Kategoriler[] deneme
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Kategorilers.Include(p => p.Menuler).OrderByDescending(p => p.kategorilertarih)
                        .Where(p => p.KategoriAktif == "true" & p.Menuler.KategoriMi == "true"
                        & p.Menuler.MenuMi == "false" & p.Menuler.Menulerid == p.Menulerid).ToArray();
                }
            }
        }

        public static veri.Slider[] slider
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Sliders.Include(p => p.Kullanicilar).OrderBy(p => p.SliderTarih)
                        .Where(p => p.AkitfMi == "true").Take(1).ToArray();
                }
            }
        }

        public static veri.FooterLinkler[] footer
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.FooterLinklers.Include(p => p.Kullanicilar).OrderByDescending(p => p.Ftarih)
                        .Where(p => p.FAktifMi == "true").ToArray();
                }
            }
        }

        public static veri.Slider[] slideriki
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Sliders.Include(p => p.Kullanicilar).OrderBy(p => p.SliderTarih)
                        .Where(p => p.AkitfMi == "true").Skip(1).ToArray();
                }
            }
        }

        public static veri.Hizmetler[] hizmetler
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Hizmetlers.Include(p => p.Kullanicilar).OrderByDescending(p => p.HizmetlerTarih)
                        .Where(p => p.HizmetlerAktif == "true").ToArray();
                }
            }
        }

        public static veri.Hizmetler[] hizmetler1
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Hizmetlers.Include(p => p.Kullanicilar).OrderByDescending(p => p.HizmetlerTarih)
                        .Where(p => p.HizmetlerAktif == "true").Take(3).ToArray();
                }
            }
        }
        public static veri.Hizmetler[] hizmetler2
        {
            get
            {
                using (burakEntities db = new burakEntities())
                {
                    return db.Hizmetlers.Include(p => p.Kullanicilar).OrderByDescending(p => p.HizmetlerTarih)
                        .Where(p => p.HizmetlerAktif == "true").Skip(3).ToArray();
                }
            }
        }
    }
}