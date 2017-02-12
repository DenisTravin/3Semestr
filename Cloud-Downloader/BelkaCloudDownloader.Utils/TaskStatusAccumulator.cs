﻿// This file isn't generated, but this comment is necessary to exclude it from StyleCop analysis.
// <auto-generated/>

namespace BelkaCloudDownloader.Utils
{
  public enum TaskStatus
  {
    NotStarted,
    InProcess,
    Success,
    Failure,
    Errors,
    Canceled
  }

  /// <summary>
  /// A helper class for setting a proper state for the task
  /// </summary>
  /// The logic is following:
  /// - If some error occurres and nothing has been extracted, the status should be Failure
  /// - If some error occurres but some information was successfully extracted, the status should be Errors
  /// - If no error occurred, the status should be success, no matter whether some info was extracted or not.
  ///
  /// So, during the process we just call SuccessItem and FailureOccurred methods on corresponding events
  /// and at the end of the process we can take the total status from the TaskStatus property.
  public sealed class TaskStatusAccumulator
  {
    /// <summary>
    /// Resets the accumulator to initial state at the srart of the process
    /// </summary>
    public void Reset()
    {
        this.someItemsSucceeded = false;
        this.failureOccurred = false;
    }

    /// <summary>
    /// Informs the accumulator that some info has been successfully extracted
    /// </summary>
    public void SuccessItem()
    {
        this.someItemsSucceeded = true;
    }

    /// <summary>
    /// Informs the accumulator that an error occurred
    /// </summary>
    public void FailureOccurred()
    {
        this.failureOccurred = true;
    }

    /// <summary>
    /// Reports the final status
    /// </summary>
    public TaskStatus Status
    {
      get
      {
        if (this.failureOccurred)
        {
          return this.someItemsSucceeded ? TaskStatus.Errors : TaskStatus.Failure;
        }

        return TaskStatus.Success;
      }
    }

    private bool someItemsSucceeded;
    private bool failureOccurred;
  }
}