-- PROCEDURE: public.CreatePatent(uuid, character varying, character varying, text, date, uuid)

-- DROP PROCEDURE IF EXISTS public."CreatePatent"(uuid, character varying, character varying, text, date, uuid);

CREATE OR REPLACE PROCEDURE public."CreatePatent"(
	IN "Id" uuid,
	IN "Code" character varying,
	IN "Title" character varying,
	IN "Content" text,
	IN "PatentDate" date,
	IN "AuthorId" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	INSERT INTO "Patent"("Id","Code","Title","Content","PatentDate")
	VALUES("Id","Code","Title","Content","PatentDate");
	COMMIT;

	INSERT INTO "PatentAuthors"("Id", "AuthorId")
	VALUES("Id", "AuthorId");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create Patent';
END
$BODY$;
ALTER PROCEDURE public."CreatePatent"(uuid, character varying, character varying, text, date, uuid)
    OWNER TO postgres;

