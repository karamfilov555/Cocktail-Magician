using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configuratons
{
    public class BarReviewConfiguration : IEntityTypeConfiguration<BarReview>
    {

        public void Configure(EntityTypeBuilder<BarReview> builder)
        {
            //builder.HasKey(r => r.Id);
            //builder.HasOne(r => r.User)
            //    .WithMany(user => user.BarReviews)
            //    .HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Bar)
               .WithMany(bar => bar.Reviews)
               .HasForeignKey(r => r.BarId);

        }

    }
}
