﻿using Video01.Helper;

namespace Video01.Model
{
    class AkademskaGodina:IEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }
    }
}
