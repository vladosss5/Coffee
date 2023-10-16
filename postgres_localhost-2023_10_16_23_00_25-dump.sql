CREATE DATABASE "Coffee" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "Coffee" OWNER TO postgres;

\connect "Coffee"

CREATE TABLE public."Category" (
    "IdCategory" integer NOT NULL,
    "Name" character varying(30) NOT NULL,
    "Photo" character varying(250)
);


ALTER TABLE public."Category" OWNER TO postgres;

CREATE SEQUENCE public."Category_IdCategory_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Category_IdCategory_seq" OWNER TO postgres;

ALTER SEQUENCE public."Category_IdCategory_seq" OWNED BY public."Category"."IdCategory";

CREATE TABLE public."Cookings" (
    "IdCooking" integer NOT NULL,
    "IdUser" integer NOT NULL,
    "IdOrder" integer NOT NULL,
    "DateAdmission" timestamp without time zone NOT NULL
);


ALTER TABLE public."Cookings" OWNER TO postgres;


CREATE SEQUENCE public."Cookings_IdCooking_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER TABLE public."Cookings_IdCooking_seq" OWNER TO postgres;

ALTER SEQUENCE public."Cookings_IdCooking_seq" OWNED BY public."Cookings"."IdCooking";

CREATE TABLE public."DishCategory" (
    "IdList" integer NOT NULL,
    "IdCategory" integer NOT NULL,
    "IdDish" integer NOT NULL
);


ALTER TABLE public."DishCategory" OWNER TO postgres;

CREATE SEQUENCE public."DishCategory_IdList_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."DishCategory_IdList_seq" OWNER TO postgres;

ALTER SEQUENCE public."DishCategory_IdList_seq" OWNED BY public."DishCategory"."IdList";

CREATE TABLE public."Dishes" (
    "IdDish" integer NOT NULL,
    "Name" character varying(30) NOT NULL,
    "Price" real NOT NULL,
    "Photo" character varying(250) NOT NULL
);


ALTER TABLE public."Dishes" OWNER TO postgres;

CREATE SEQUENCE public."Dishes_IdDish_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Dishes_IdDish_seq" OWNER TO postgres;

--
-- Name: Dishes_IdDish_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Dishes_IdDish_seq" OWNED BY public."Dishes"."IdDish";


--
-- Name: OrderDish; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."OrderDish" (
    "IdList" integer NOT NULL,
    "IdDish" integer NOT NULL,
    "IdOrder" integer NOT NULL,
    "Count" integer NOT NULL
);


ALTER TABLE public."OrderDish" OWNER TO postgres;

--
-- Name: OrderDish_IdList_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."OrderDish_IdList_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."OrderDish_IdList_seq" OWNER TO postgres;

--
-- Name: OrderDish_IdList_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."OrderDish_IdList_seq" OWNED BY public."OrderDish"."IdList";


--
-- Name: Orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Orders" (
    "IdOrder" integer NOT NULL,
    "FullPrice" real NOT NULL,
    "DateAndTime" timestamp without time zone NOT NULL,
    "IdStstus" integer NOT NULL
);


ALTER TABLE public."Orders" OWNER TO postgres;

--
-- Name: Orders_IdOrder_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Orders_IdOrder_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Orders_IdOrder_seq" OWNER TO postgres;

--
-- Name: Orders_IdOrder_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Orders_IdOrder_seq" OWNED BY public."Orders"."IdOrder";


--
-- Name: Posts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Posts" (
    "IdPost" integer NOT NULL,
    "Name" character varying(20) NOT NULL
);


ALTER TABLE public."Posts" OWNER TO postgres;

--
-- Name: Posts_IdPost_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Posts_IdPost_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Posts_IdPost_seq" OWNER TO postgres;

--
-- Name: Posts_IdPost_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Posts_IdPost_seq" OWNED BY public."Posts"."IdPost";


--
-- Name: Promotions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Promotions" (
    "IdPromotion" integer NOT NULL,
    "IdDish" integer NOT NULL,
    "FinishDate" timestamp without time zone NOT NULL,
    "DiscountPercent" integer NOT NULL,
    "TotalPrice" double precision NOT NULL
);


ALTER TABLE public."Promotions" OWNER TO postgres;

--
-- Name: Promotions_IdPromotion_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Promotions_IdPromotion_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Promotions_IdPromotion_seq" OWNER TO postgres;

--
-- Name: Promotions_IdPromotion_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Promotions_IdPromotion_seq" OWNED BY public."Promotions"."IdPromotion";


--
-- Name: StatusesOrder; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."StatusesOrder" (
    "IdStatus" integer NOT NULL,
    "Name" character varying(25) NOT NULL
);


ALTER TABLE public."StatusesOrder" OWNER TO postgres;

--
-- Name: StatusesOrder_IdStatus_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."StatusesOrder_IdStatus_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."StatusesOrder_IdStatus_seq" OWNER TO postgres;

--
-- Name: StatusesOrder_IdStatus_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."StatusesOrder_IdStatus_seq" OWNED BY public."StatusesOrder"."IdStatus";


--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "IdUser" integer NOT NULL,
    "Login" character varying(30) NOT NULL,
    "Password" character varying(30) NOT NULL,
    "FName" character varying(30) NOT NULL,
    "SName" character varying(30) NOT NULL,
    "LName" character varying(30),
    "IdPost" integer NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Name: Users_IdUser_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Users_IdUser_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Users_IdUser_seq" OWNER TO postgres;

--
-- Name: Users_IdUser_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Users_IdUser_seq" OWNED BY public."Users"."IdUser";


--
-- Name: Category IdCategory; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Category" ALTER COLUMN "IdCategory" SET DEFAULT nextval('public."Category_IdCategory_seq"'::regclass);


--
-- Name: Cookings IdCooking; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cookings" ALTER COLUMN "IdCooking" SET DEFAULT nextval('public."Cookings_IdCooking_seq"'::regclass);


--
-- Name: DishCategory IdList; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DishCategory" ALTER COLUMN "IdList" SET DEFAULT nextval('public."DishCategory_IdList_seq"'::regclass);


--
-- Name: Dishes IdDish; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Dishes" ALTER COLUMN "IdDish" SET DEFAULT nextval('public."Dishes_IdDish_seq"'::regclass);


--
-- Name: OrderDish IdList; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderDish" ALTER COLUMN "IdList" SET DEFAULT nextval('public."OrderDish_IdList_seq"'::regclass);


--
-- Name: Orders IdOrder; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders" ALTER COLUMN "IdOrder" SET DEFAULT nextval('public."Orders_IdOrder_seq"'::regclass);


--
-- Name: Posts IdPost; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Posts" ALTER COLUMN "IdPost" SET DEFAULT nextval('public."Posts_IdPost_seq"'::regclass);


--
-- Name: Promotions IdPromotion; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Promotions" ALTER COLUMN "IdPromotion" SET DEFAULT nextval('public."Promotions_IdPromotion_seq"'::regclass);


--
-- Name: StatusesOrder IdStatus; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."StatusesOrder" ALTER COLUMN "IdStatus" SET DEFAULT nextval('public."StatusesOrder_IdStatus_seq"'::regclass);


--
-- Name: Users IdUser; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users" ALTER COLUMN "IdUser" SET DEFAULT nextval('public."Users_IdUser_seq"'::regclass);


--
-- Data for Name: Category; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Category" VALUES (2, 'Напитки', 'AssetsUser/beverages.png');
INSERT INTO public."Category" VALUES (3, 'Десерты', 'AssetsUser/Dessert.png');


--
-- Data for Name: Cookings; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: DishCategory; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."DishCategory" VALUES (1, 2, 1);
INSERT INTO public."DishCategory" VALUES (2, 3, 2);
INSERT INTO public."DishCategory" VALUES (3, 3, 3);


--
-- Data for Name: Dishes; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Dishes" VALUES (1, 'Капучино', 100, 'AssetsUser/Kapuchino.jpg');
INSERT INTO public."Dishes" VALUES (2, 'Кекс', 40, 'AssetsUser/Keks.jpg');
INSERT INTO public."Dishes" VALUES (3, 'Синабон', 80, 'AssetsUser/Synabon.jpeg');


--
-- Data for Name: OrderDish; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."OrderDish" VALUES (1, 1, 2, 2);
INSERT INTO public."OrderDish" VALUES (2, 2, 2, 1);


--
-- Data for Name: Orders; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Orders" VALUES (2, 240, '2023-10-16 22:57:08.79668', 1);


--
-- Data for Name: Posts; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Posts" VALUES (1, 'Администратор');


--
-- Data for Name: Promotions; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Promotions" VALUES (1, 3, '2023-10-17 00:00:00', 20, 0);
INSERT INTO public."Promotions" VALUES (2, 2, '2023-10-17 00:00:00', 20, 0);


--
-- Data for Name: StatusesOrder; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."StatusesOrder" VALUES (1, 'Принят');
INSERT INTO public."StatusesOrder" VALUES (2, 'Готовится');
INSERT INTO public."StatusesOrder" VALUES (3, 'Готово');
INSERT INTO public."StatusesOrder" VALUES (4, 'Выдано');


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Users" VALUES (2, '1', '1', 'Владислав', 'Кипоров', 'Александрович', 1);


--
-- Name: Category_IdCategory_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Category_IdCategory_seq"', 1, false);


--
-- Name: Cookings_IdCooking_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Cookings_IdCooking_seq"', 1, false);


--
-- Name: DishCategory_IdList_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."DishCategory_IdList_seq"', 3, true);


--
-- Name: Dishes_IdDish_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Dishes_IdDish_seq"', 3, true);


--
-- Name: OrderDish_IdList_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."OrderDish_IdList_seq"', 2, true);


--
-- Name: Orders_IdOrder_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Orders_IdOrder_seq"', 2, true);


--
-- Name: Posts_IdPost_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Posts_IdPost_seq"', 1, false);


--
-- Name: Promotions_IdPromotion_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Promotions_IdPromotion_seq"', 2, true);


--
-- Name: StatusesOrder_IdStatus_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."StatusesOrder_IdStatus_seq"', 1, false);


--
-- Name: Users_IdUser_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Users_IdUser_seq"', 2, true);


--
-- Name: Category Category_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Category"
    ADD CONSTRAINT "Category_pkey" PRIMARY KEY ("IdCategory");


--
-- Name: Cookings Cookings_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cookings"
    ADD CONSTRAINT "Cookings_pkey" PRIMARY KEY ("IdCooking");


--
-- Name: DishCategory DishCategory_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DishCategory"
    ADD CONSTRAINT "DishCategory_pkey" PRIMARY KEY ("IdList");


--
-- Name: Dishes Dishes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Dishes"
    ADD CONSTRAINT "Dishes_pkey" PRIMARY KEY ("IdDish");


--
-- Name: OrderDish OrderDish_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderDish"
    ADD CONSTRAINT "OrderDish_pkey" PRIMARY KEY ("IdList");


--
-- Name: Orders Orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_pkey" PRIMARY KEY ("IdOrder");


--
-- Name: Posts Posts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Posts"
    ADD CONSTRAINT "Posts_pkey" PRIMARY KEY ("IdPost");


--
-- Name: Promotions Promotions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Promotions"
    ADD CONSTRAINT "Promotions_pkey" PRIMARY KEY ("IdPromotion");


--
-- Name: StatusesOrder StatusesOrder_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."StatusesOrder"
    ADD CONSTRAINT "StatusesOrder_pkey" PRIMARY KEY ("IdStatus");


--
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("IdUser");


--
-- Name: Cookings Cookings_IdOrder_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cookings"
    ADD CONSTRAINT "Cookings_IdOrder_fkey" FOREIGN KEY ("IdOrder") REFERENCES public."Orders"("IdOrder");


--
-- Name: Cookings Cookings_IdUser_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cookings"
    ADD CONSTRAINT "Cookings_IdUser_fkey" FOREIGN KEY ("IdUser") REFERENCES public."Users"("IdUser");


--
-- Name: DishCategory DishCategory_IdCategory_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DishCategory"
    ADD CONSTRAINT "DishCategory_IdCategory_fkey" FOREIGN KEY ("IdCategory") REFERENCES public."Category"("IdCategory");


--
-- Name: DishCategory DishCategory_IdDish_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DishCategory"
    ADD CONSTRAINT "DishCategory_IdDish_fkey" FOREIGN KEY ("IdDish") REFERENCES public."Dishes"("IdDish");


--
-- Name: OrderDish OrderDish_IdDish_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderDish"
    ADD CONSTRAINT "OrderDish_IdDish_fkey" FOREIGN KEY ("IdDish") REFERENCES public."Dishes"("IdDish");


--
-- Name: OrderDish OrderDish_IdOrder_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."OrderDish"
    ADD CONSTRAINT "OrderDish_IdOrder_fkey" FOREIGN KEY ("IdOrder") REFERENCES public."Orders"("IdOrder");


--
-- Name: Orders Orders_IdStstus_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Orders"
    ADD CONSTRAINT "Orders_IdStstus_fkey" FOREIGN KEY ("IdStstus") REFERENCES public."StatusesOrder"("IdStatus");


--
-- Name: Promotions Promotions_IdDish_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Promotions"
    ADD CONSTRAINT "Promotions_IdDish_fkey" FOREIGN KEY ("IdDish") REFERENCES public."Dishes"("IdDish");


--
-- Name: Users Users_IdPost_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_IdPost_fkey" FOREIGN KEY ("IdPost") REFERENCES public."Posts"("IdPost");


--
-- PostgreSQL database dump complete
--

