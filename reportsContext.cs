using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using southSoundWebsite.Models;
namespace southSoundWebsite
{
    public partial class reportsContext : DbContext
    {

        protected readonly IConfiguration _conf;
       
       
        public reportsContext(DbContextOptions<reportsContext> options)
            : base(options)
        {            
        }

      
        public virtual DbSet<Ex2> Ex2s { get; set; } = null!;
   


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ex2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ex2");

                entity.Property(e => e.IsrcКонтента)
                    .HasMaxLength(50)
                    .HasColumnName("ISRC_контента");

                entity.Property(e => e.UpcАльбомаКодТрекаПоБазеЛицензиара)
                    .HasMaxLength(1)
                    .HasColumnName("UPC_альбома_Код_трека_по_базе_лицензиара");

                entity.Property(e => e.ВидИспользованияКонтентаЗагрузкаСтриммингПодпискаРадио)
                    .HasMaxLength(50)
                    .HasColumnName("Вид_использования_контента_Загрузка_стримминг_подписка_радио");

                entity.Property(e => e.ВознаграждениеВРубБезНдс)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Вознаграждение_в_руб_без_НДС");

                entity.Property(e => e.ДоляЛицензиара)
                    .HasMaxLength(50)
                    .HasColumnName("Доля_Лицензиара");

                entity.Property(e => e.Исполнитель).HasMaxLength(50);

                entity.Property(e => e.КодЛицензиата)
                    .HasMaxLength(50)
                    .HasColumnName("код_Лицензиата");

                entity.Property(e => e.КоличествоЗагрузокПрослушиваний).HasColumnName("Количество_загрузок_прослушиваний");

                entity.Property(e => e.Копирайт).HasMaxLength(50);

                entity.Property(e => e.НазваниеАльбома)
                    .HasMaxLength(50)
                    .HasColumnName("Название_альбома");

                entity.Property(e => e.НазваниеТрека)
                    .HasMaxLength(50)
                    .HasColumnName("Название_трека");

                entity.Property(e => e.Площадка).HasMaxLength(50);

                entity.Property(e => e.СуммаДенежныхСредствПолученныхЛицензиатом)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Сумма_денежных_средств_полученных_Лицензиатом");

                entity.Property(e => e.Территория).HasMaxLength(50);

                entity.Property(e => e.ТипКонтента)
                    .HasMaxLength(50)
                    .HasColumnName("Тип_контента");
            });
          
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
