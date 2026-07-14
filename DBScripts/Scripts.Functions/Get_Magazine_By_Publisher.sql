-- FUNCTION: public.Get_Magazine_By_Publisher(uuid)

-- DROP FUNCTION IF EXISTS public."Get_Magazine_By_Publisher"(uuid);

CREATE OR REPLACE FUNCTION public."Get_Magazine_By_Publisher"(
	"PublisherId" uuid)
    RETURNS TABLE("Issn" character varying, "Title" character varying, "PublishDate" date, "EndOfPublish" date, "PublisherName" character varying) 
    LANGUAGE 'sql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
SELECT 
    "Issn",
    "Title",
    "PublishDate",
    "EndOfPublish",
    "PublisherName"
FROM public."MagazineView"
WHERE "PublisherId" = "PublisherId";
$BODY$;

ALTER FUNCTION public."Get_Magazine_By_Publisher"(uuid)
    OWNER TO postgres;

