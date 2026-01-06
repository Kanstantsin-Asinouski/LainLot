using System;
using System.Collections.Generic;

namespace DatabaseProvider.Models;

public partial class About
{
    public int Id { get; set; }

    public int FkLanguages { get; set; }

    public string Header { get; set; } = null!;

    public string Text { get; set; } = null!;

    public virtual Language FkLanguagesNavigation { get; set; } = null!;
}
