using Microsoft.EntityFrameworkCore.Migrations;

namespace EStore.Service.Migrations
{
    public partial class EncryptProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

           --IF(EXISTS (Select 0 from sys.column_encryption_keys Where Name ='TestColumnKey'))
           --
           --BEGIN
           --   Alter table Product.Product
           --                        Drop column Name

           --    Alter table Product.Product
           --    Add Name nvarchar(4000) COLLATE Latin1_General_BIN2 ENCRYPTED WITH (  
           --                ENCRYPTION_TYPE = DETERMINISTIC,  
           --                ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256',
		   --                COLUMN_ENCRYPTION_KEY = TestColumnKey)  NULL
           --       
           --END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                --Deleting the record so the changes being performed.
                    Delete from product.Product

                    ALTER TABLE [Product].[Product]
	                    DROP COLUMN Name

                    ALTER TABLE [Product].[Product]
	                    ADD Name NVARCHAR(450) NOT NULL

");         
        }
    }
}
