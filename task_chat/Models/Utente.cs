using System;
using System.Collections.Generic;

namespace task_chat.Models;

public partial class Utente
{
    public int UtenteId { get; set; }

    public string? CodiceUtente { get; set; } = Guid.NewGuid().ToString().ToUpper();

    public string Username { get; set; } = null!;

    public string PasswordUtente { get; set; } = null!;

    public bool? IsDeleted { get; set; }
}
