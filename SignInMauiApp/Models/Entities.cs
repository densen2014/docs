using FreeSql.DataAnnotations;
using System.ComponentModel;

namespace SignInMauiApp.Models;

//包含租户、用户、签到记录的数据模型

public class Tenant
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? TaxNumber { get; set; }  

    public string? Account { get; set; }

    public string? Phone { get; set; }  
}

public class User
{
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? TaxNumber { get; set; } 
    public string? Phone { get; set; }
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
    public DateTime? SignInTime { get; set; }
    public DateTime? SignOutTime { get; set; } 
    public string? TaxNumber { get; set; }

    /// <summary>
    /// 上午签到时间
    /// </summary>
    public DateTime? MorningSignInTime { get; set; } // 上午签到时间

    /// <summary>
    /// 上午签出时间
    /// </summary>
    public DateTime? MorningSignOutTime { get; set; } // 上午签出时间

    /// <summary>
    /// 下午签到时间
    /// </summary>
    public DateTime? AfternoonSignInTime { get; set; } // 下午签到时间

    /// <summary>
    /// 下午签出时间
    /// </summary>
    public DateTime? AfternoonSignOutTime { get; set; } // 下午签出时间

    /// <summary>
    /// 当日总工作时长
    /// </summary>
    public TimeSpan? TotalWorkDuration { get; set; } // 当日总工作时长

    /// <summary>
    /// 正常工时
    /// </summary>
    public TimeSpan? NormalWorkDuration { get; set; } // 正常工时

    /// <summary>
    /// 补充工时
    /// </summary>
    public TimeSpan? ExtraWorkDuration { get; set; } // 补充工时
}

public class SignInWeb
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Action { get; set; } = "signin";
}
