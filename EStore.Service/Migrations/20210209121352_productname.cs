using Microsoft.EntityFrameworkCore.Migrations;

namespace EStore.Service.Migrations
{
    public partial class productname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

           IF(EXISTS (Select 0 from sys.column_encryption_keys Where Name ='EStoreColumnEncryptionKey'))
           
           BEGIN
              Alter table Product.product
                                   Drop column Name
               Alter table Product.product
               Add Name nvarchar(4000) COLLATE Latin1_General_BIN2 ENCRYPTED WITH (  
                           ENCRYPTION_TYPE = DETERMINISTIC,  
                           ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256',
		                   COLUMN_ENCRYPTION_KEY = EStoreColumnEncryptionKey)  NULL
                  
           END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                --Deleting the record so the changes being performed.
                    Delete from product.encryptionTest

                    ALTER TABLE [Product].[product]
	                    DROP COLUMN Name

                    ALTER TABLE [Product].[product]
	                    ADD Name NVARCHAR(450)  NULL

");
        }
    }
}
