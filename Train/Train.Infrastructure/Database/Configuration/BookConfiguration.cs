using Domain.Model.Model.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class BookConfiguration :IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Book");
        builder.Property(x => x.Id);

        // object value convert
        builder.OwnsOne(x => x.BookTitle, q =>
        {
            q.Property(a => a.Title).HasMaxLength(100);
        });
    }
}