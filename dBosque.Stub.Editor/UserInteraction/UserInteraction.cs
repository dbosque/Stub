using System;

namespace dBosque.Stub.Editor.UserInteraction
{
    internal class UserInteractor
    {

        protected UserInteractor(string description)
        {
            Description = description;
        }

        internal string Description { get; set; }      

        public static UserInteractor NoDataChanged => new UserInteractor("No data to save.");
        public static UserInteractor InvalidUri => new UserInteractor("'{0}' is not a valid uri.");

        public static UserInteractor ErrorOnSave => new UserInteractor("Error updateing the node. Please refresh first.");
        public static UserInteractor RegexNoNamedGroups => new UserInteractor("No named groups where found in the regular expression." + Environment.NewLine + "E.g. : abc(?<name>.*) ") ;
        public static UserInteractor RegexNoMatch => new UserInteractor("No matches where found.") ;
        public static UserInteractor RegexMatch => new UserInteractor("The following matches where found : " + Environment.NewLine + "{0}");
        public static UserInteractor RegexError => new UserInteractor("Invalid regex : {0}") ;
        public static UserInteractor XpathDeleteConfirmation => new UserInteractor("Are you sure to delete Xpath {0}?");
        public static UserInteractor XpathStillInUse => new UserInteractor("There are still combinaties of templates that use Xpath {0}");
        public static UserInteractor XpathChange => new UserInteractor("There are still combinaties of templates that use Xpath {0} are you sure to change it?");
        
        public static UserInteractor XpathDeleteOneByOne => new UserInteractor("Xpaths can only be deleted one by one.") ;
        public static UserInteractor AddXpathToList => new UserInteractor("Add Xpath {0} to the list?");
        public static UserInteractor XpathExists => new UserInteractor("Xpath {0} already exists.") ;
        public static UserInteractor NoXpathsSelected => new UserInteractor("No xpaths selected.");
        public static UserInteractor NoXpathsRegExSelected => new UserInteractor("No xpaths selected or Regular Expression entered.");
        public static UserInteractor NoDescription => new UserInteractor("Empty description.") ;

        public static UserInteractor DeleteTemplateAll => new UserInteractor("There are Combinaties with a different UserInteractionType but with the same template, these will also be deleted.");
        public static UserInteractor DeleteTemplate => new UserInteractor("Are you sure you want to remove this template and all combinaties from the stub?") ;
        public static UserInteractor DeleteCombination => new UserInteractor("Are you sure you want to remove this combination from the stub?") ;
        public static UserInteractor NamespaceExists => new UserInteractor("A UserInteractiontype with given namespace '{0}' and rootnode '{1}' already exists.") ;
        public static UserInteractor NamesspaceCreated => new UserInteractor("The following stubdefinition will be created / changed :" + Environment.NewLine +
                                                                                                 "Namespace    : {0}" + Environment.NewLine +
                                                                                                 "Rootnode     : {1}" + Environment.NewLine +
                                                                                                 "Description  : {2}");
        public static UserInteractor UnableToDeleteUserInteractionType => new UserInteractor("Unable to remove the stubdefinition with description '{0}' because there are templates using it.");

        public static UserInteractor DeleteUserInteractionType => new UserInteractor("Are you sure you want to remove the stubdefinition with description '{0}'?");

        public static UserInteractor UnableToDeleteMessageType => new UserInteractor("Unable to remove the stubdefinition with description '{0}' because there are templates using it.");

        public static UserInteractor DeleteMessageType => new UserInteractor("Are you sure you want to remove the stubdefinition with description '{0}'?");


    }
}
