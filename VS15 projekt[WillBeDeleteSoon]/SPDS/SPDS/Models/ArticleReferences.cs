namespace SPDS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticleReferences
    {
        public int ArticleReferencesId { get; set; }

        public string Description { get; set; }

        public int DatasetArticleReferences_ArticleReferences_DatasetId { get; set; }

        public virtual Dataset Dataset { get; set; }
    }
}
