-- PROCEDURE: public.UpdatePatent(uuid, character varying, character varying, text, date, uuid)

-- DROP PROCEDURE IF EXISTS public."UpdatePatent"(uuid, character varying, character varying, text, date, uuid);

CREATE OR REPLACE PROCEDURE public."UpdatePatent"(
	IN "Id" uuid,
	IN "Code" character varying,
	IN "Title" character varying,
	IN "Content" text,
	IN "PatentDate" date,
	IN "AuthorId" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "Patent"
    SET
        "Code"    = "Code",
        "Title"   = "Title",
        "Content" = "Content",
		"PatentDate" = "PatentDate"
    WHERE "Id" = "Id";

	COMMIT;

	UPDATE "PatentAuthors" SET "AuthorId" = "AuthorId" WHERE "PatentId" = "Id";

	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update Patent';
END
$BODY$;
ALTER PROCEDURE public."UpdatePatent"(uuid, character varying, character varying, text, date, uuid)
    OWNER TO postgres;

