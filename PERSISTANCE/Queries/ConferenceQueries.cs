namespace PERSISTANCE.Queries;

public class ConferenceQueries
{
    public const string CREATE_CONFERENCE = @"usp_CreateConference";
    public const string GET_CONFERENCES = @"usp_GetConferences";
    public const string GET_CONFERENCE = @"usp_GetConference";
    public const string REGISTER_AT_CONFERENCE = "usp_RegisterAtConference";
    public const string GET_MY_CONFERENCES = "usp_GetMyConferences";
    public const string GET_PARTICIPANTS = @"usp_GetParticipants";
    public const string GET_SPEAKERS = "usp_GetSpeakers";
    public const string GET_MANAGERS = "usp_GetManagers";
    public const string ADD_NAV_ITEM = "usp_AddNavItem";
}
