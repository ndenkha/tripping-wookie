﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IDependencyConsumer
    {
        void Accept(IKernel kernel);
    }
}
