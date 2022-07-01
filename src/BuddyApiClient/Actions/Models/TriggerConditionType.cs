namespace BuddyApiClient.Actions.Models
{
    public enum TriggerConditionType
    {
        /// <summary>
        ///     Run without any condition.
        /// </summary>
        Always,

        /// <summary>
        ///     Run only if there were changes in the repository since last execution.
        /// </summary>
        OnChange,

        /// <summary>
        ///     Run only if there were changes to specific paths in the repository since last execution.
        /// </summary>
        OnChangeAtPath,

        /// <summary>
        ///     Run only if the variable is equal to a specific value.
        /// </summary>
        VariableIs,

        /// <summary>
        ///     Run only if the variable is not equal to a specific value.
        /// </summary>
        VariableIsNot,

        /// <summary>
        ///     Run only if the variable contains a specific value.
        /// </summary>
        VariableContains,

        /// <summary>
        ///     Run only if the variable does not contain a specific value.
        /// </summary>
        VariableContainsNot,

        /// <summary>
        ///     Run only on specific day and/or hour.
        /// </summary>
        DateTime,

        /// <summary>
        ///     Run only if a specified pipeline passes with the same revision.
        /// </summary>
        SuccessPipeline
    }
}