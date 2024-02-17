using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRabbitMQService
    {
        public void SendEvent<T>(T message);

        public void GetEvent();

    }
}
