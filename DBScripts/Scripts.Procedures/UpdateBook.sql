-- PROCEDURE: public.UpdateBook(uuid, character varying, character varying, text, uuid, uuid)

-- DROP PROCEDURE IF EXISTS public."UpdateBook"(uuid, character varying, character varying, text, uuid, uuid);

CREATE OR REPLACE PROCEDURE public."UpdateBook"(
	IN "Id" uuid,
	IN "Isbn" character varying,
	IN "Title" character varying,
	IN "Content" text,
	IN "AuthorId" uuid,
	IN "PublisherId" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "Book"
    SET
        "Isbn"    = "Isbn",
        "Title"   = "Title",
        "Content" = "Content"
    WHERE "Id" = "Id";

	COMMIT;

	UPDATE "BookAuthors" SET "AuthorId" = "AuthorId" WHERE "BookId" = "Id";

	UPDATE "BookPublisher" SET "PublisherId" = "PublisherId" WHERE "BookId" = "Id";
	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update Book';
END
$BODY$;
ALTER PROCEDURE public."UpdateBook"(uuid, character varying, character varying, text, uuid, uuid)
    OWNER TO postgres;

