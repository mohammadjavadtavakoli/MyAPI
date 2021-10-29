﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebFramework.Common
{
    public enum ApiResualtStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 0,
        [Display(Name = "خطایی در سرور رخ داده است")]
        ServerError = 1,
        [Display(Name = "پارامتر های ارسالی معتبر نیست")]
        BadRequest = 2,
        [Display(Name = "یافت نشد")]
        NotFound = 3,
        [Display(Name = "لیست خالی است")]
        ListEmpty = 4,
        [Display(Name = "خطایی در پردازش رخ داده است")]
        LogicError = 4,
        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 6
    }
}
