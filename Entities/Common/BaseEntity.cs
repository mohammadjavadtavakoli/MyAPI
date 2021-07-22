using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public interface IEntity
    {

    }
   public abstract class BaseEntity<Tkey>:IEntity
    {
        public Tkey Id { get; set; }
    }

    public abstract class BaseEntity: BaseEntity<int>
    {

    }
}
