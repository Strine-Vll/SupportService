using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Attachment
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public byte[] Content { get; set; }

    #region Navigation Properties

    public int CommentId { get; set; }

    public Comment Comment { get; set; }

    #endregion
}
