using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class Language
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string DateFormat { get; set; } = null!;

    public string TimeFormat { get; set; } = null!;

    public virtual ICollection<About> Abouts { get; set; } = new List<About>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<ProductTranslation> ProductTranslations { get; set; } = new List<ProductTranslation>();
}
