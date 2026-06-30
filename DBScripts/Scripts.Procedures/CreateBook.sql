-- PROCEDURE: public.CreateBook(uuid, character varying, character varying, text, uuid, uuid)

-- DROP PROCEDURE IF EXISTS public."CreateBook"(uuid, character varying, character varying, text, uuid, uuid);

CREATE OR REPLACE PROCEDURE public."CreateBook"(
	IN "Id" uuid,
	IN "Isbn" character varying,
	IN "Title" character varying,
	IN "Content" text,
	IN "AuthorId" uuid,
	IN "PulisherId" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	INSERT INTO "Book"("Id", "Isbn", "Title", "Content")
    VALUES ("Id", "Isbn", "Title", "Content");
	COMMIT;

    INSERT INTO "BookAuthors"("BookId", "AuthorId")
    VALUES ("Id", "AuthorId");
	COMMIT;

    INSERT INTO "BookPublisher"("BookId", "PublisherId")
    VALUES ("Id", "PublisherId");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create Book';

END
$BODY$;
ALTER PROCEDURE public."CreateBook"(uuid, character varying, character varying, text, uuid, uuid)
    OWNER TO postgres;

