﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO.FormModel
{
    public class UpdateProfileAvatarModel
    {
        public IFormFile? image { get; set; }
    }
}
