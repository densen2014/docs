using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace SignInMauiApp.Models;

//包含租户、用户、签到记录的数据模型

public class Tenant
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class User
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int TenantId { get; set; }
    public bool IsAdmin { get; internal set; }
}

public enum SignTypeEnum
{
    [Description("上班签到")]
    SignInWork,
    [Description("下班签退")]
    SignOutWork,
    [Description("外出签到")]
    SignInOut,
    [Description("外出签退")]
    SignOutIn,
    [Description("其他签到")]
    SignInOther,
    [Description("其他签退")]
    SignOutOther,
}

public class SignInRecord
{
    [DisplayName("记录ID")]
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }

    [DisplayName("用户ID")]
    public int UserId { get; set; }

    [DisplayName("租户ID")]
    public int TenantId { get; set; }

    [DisplayName("数据类型")]
    public SignTypeEnum SignType { get; set; }= SignTypeEnum.SignInWork;

    [DisplayName("时间")]
    public DateTime? SignInTime { get; set; }

    public Guid TimestampToken { get; set; } = Guid.NewGuid();

}

public class SignInReportItem
{
    public string? Username { get; set; }
    public string? TenantName { get; set; }
    public SignTypeEnum SignType { get; set; }
    public DateTime? SignInTime { get; set; }
    public DateTime? SignOutTime { get; set; }
}

public class SignInWeb
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Action { get; set; } = "signin";
}
