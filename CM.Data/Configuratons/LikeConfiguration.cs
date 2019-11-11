//using CM.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace CM.Data.Configuratons
//{
//    public class LikeConfiguration : IEntityTypeConfiguration<Like>
//    {
//        public void Configure(EntityTypeBuilder<Like> builder)
//        {
//            builder
//                 .Property(book => book.Category)
//                 .IsRequired()
//                 .HasConversion(b => b.ToString(), b => (CategoryType)Enum.Parse(typeof(CategoryType), b));
//        }
//    }
//}
