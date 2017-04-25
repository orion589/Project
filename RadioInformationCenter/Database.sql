---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE Software (
    SoftwareId uuid UNIQUE NOT NULL,
    SoftwareKey uuid NOT NULL,
    Node text NOT NULL,
    SoftwareName text NOT NULL,
    PRIMARY KEY (SoftwareId) 	
);

CREATE INDEX SoftwareId_idx ON Software (SoftwareId);
CREATE INDEX SoftwareKey_idx ON Software (SoftwareKey);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE SoftwareMessageLog (
    SoftwareMessageLogId uuid UNIQUE NOT NULL,
	Culture integer,
    Livel smallint NOT NULL,
    CaptureDate timestamp with time zone NOT NULL,
	OperatingDate timestamp with time zone,
	Category text,
    Message text NOT NULL,
    SoftwareId uuid REFERENCES Software (SoftwareId) ON DELETE CASCADE,
    PRIMARY KEY (SoftwareMessageLogId)
);

CREATE INDEX SoftwareMessageId_idx ON SoftwareMessageLog (SoftwareMessageLogId);
CREATE INDEX SoftwareMessageLogCaptureDate_idx ON SoftwareMessageLog (CaptureDate);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE SoftwareDataLog (
    SoftwareDataLogId uuid UNIQUE NOT NULL,
    Direction smallint NOT NULL,
	Livel smallint NOT NULL,
	Protocol text NOT NULL,
	Address text,
	Culture integer,
	Remark text,
    Raw bytea,
	OperatingDate timestamp with time zone,
    CaptureDate timestamp with time zone NOT NULL,
    SoftwareId uuid REFERENCES Software (SoftwareId) ON DELETE CASCADE,
    PRIMARY KEY (SoftwareDataLogId)
);

CREATE INDEX SoftwareDataLogId_idx ON SoftwareDataLog (SoftwareDataLogId);
CREATE INDEX SoftwareDataLogCaptureDate_idx ON SoftwareDataLog (CaptureDate);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirTrace (
	AirTraceId uuid UNIQUE NOT NULL,
	Number integer NOT NULL,
	IsTrain boolean NOT NULL,
	CreateDate timestamp with time zone  NOT NULL,
	RemoveDate timestamp with time zone,
	RemoveReason smallint,
	PRIMARY KEY (AirTraceId)
);

CREATE INDEX AirTraceId_idx ON AirTrace (AirTraceId);
CREATE INDEX AirTraceNumber_idx ON AirTrace (Number);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirCoordinate (
	AirCoordinateId uuid UNIQUE NOT NULL,
	Number integer NOT NULL,
	Latitude double precision NOT NULL,
	Longitude double precision NOT NULL,
	Altitude double precision NOT NULL,
	CaptureDate  timestamp with time zone  NOT NULL,	
	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (AirCoordinateId)
);

CREATE INDEX AirCoordinateId_idx ON AirCoordinate (AirCoordinateId);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirAbonent (
	AirAbonentId uuid UNIQUE NOT NULL,
	AbonentId uuid NOT NULL,
	BeginDate timestamp with time zone  NOT NULL,
	EndDate timestamp with time zone ,
	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (AirAbonentId)
);

CREATE INDEX AirAbonentId_idx ON AirAbonent (AirAbonentId);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirIndex (
	AirIndexId uuid UNIQUE NOT NULL,
	Index integer NOT NULL,
	BeginDate timestamp with time zone  NOT NULL,
	EndDate timestamp with time zone ,
	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (AirIndexId)
);

CREATE INDEX AirIndexId_idx ON AirIndex (AirIndexId);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirUnionNumber (
	AirUnionNumberId uuid UNIQUE NOT NULL,
	Number integer NOT NULL,
	BeginDate timestamp with time zone  NOT NULL,
	EndDate timestamp with time zone ,
	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (AirUnionNumberId)
);

CREATE INDEX AirUnionNumberId_idx ON AirUnionNumber (AirUnionNumberId);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirNotify (
	airnotifyid uuid NOT NULL,
	Number integer NOT NULL,
	Type smallint NOT NULL,
	AbonentId uuid NOT NULL,
	Notifydate timestamp with time zone,
	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (AirNotifyId)
);

CREATE INDEX AirNotifyId_idx ON AirNotify (AirNotifyId);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
CREATE TABLE AirParameter (
	AirParameterId uuid UNIQUE NOT NULL,
	AirCoordinateId uuid, 
    Number integer NOT NULL,
    UnionNumber integer NOT NULL,
    Count smallint NOT NULL,
    Course  double precision NOT NULL,
    Speed smallint NOT NULL,
    Fuel smallint NOT NULL,
    AirObjectType integer NOT NULL,
    Index integer NOT NULL,
    Maneuver integer NOT NULL,
    Offence integer NOT NULL,
    Distress integer NOT NULL,
    Interference integer NOT NULL,
    Location integer NOT NULL,
    Action integer NOT NULL,
    Nationality integer NOT NULL,
    Source integer NOT NULL,
    MeasurementAltitude integer NOT NULL,
    IsTrain boolean NOT NULL,
    IsExtrapolation boolean NOT NULL,
    IsAntiRlsTool boolean NOT NULL,
    IsBorderIntruder boolean NOT NULL,
    IsOvercount boolean NOT NULL,
    IsFictitiousAltitude boolean NOT NULL,
    LocationTime timestamp with time zone NOT NULL,
	CaptureDate timestamp with time zone NOT NULL,
	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY (AirParameterId, CaptureDate)
);

CREATE INDEX AirParameterId_idx ON AirParameter (AirParameterId);
CREATE INDEX AirParameterNumber_idx ON AirParameter (Number);
CREATE INDEX AirParameterUnionNumber_idx ON AirParameter (UnionNumber);

---------------------------------------------------------------------------------------------------------------
--
---------------------------------------------------------------------------------------------------------------
--CREATE TABLE RawCodogram (
--	RawCodogramId uuid UNIQUE NOT NULL,
--	Raw bytea,
--	CaptureDate timestamp with time zone NOT NULL,
--	AirTraceId uuid REFERENCES AirTrace (AirTraceId) ON DELETE CASCADE,
--	PRIMARY KEY (RawCodogramId)
--);

--CREATE INDEX RawCodogramId_idx ON RawCodogram (RawCodogramId);
--CREATE INDEX RawCodogramCaptureDate_idx ON RawCodogram (CaptureDate);