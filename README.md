## Long Running Task Sample App

This sample application demonstrates how to implement a long running task in Xamarin.Forms for both iOS and Android platforms. The app utilizes a service injected into the Xamarin.Forms project to run a counter even when the app is in the background.

### Inputs

The application responds to two messages sent via the `MessagingCenter`:

*   **StartLongRunningTaskMessage:** Initiates the long-running task. This message is typically sent when the user presses the "Start" button on the main page.
*   **StopLongRunningTaskMessage:**  Stops the long-running task. This message is usually sent when the user interacts with the "Stop" button on the main page.

### Outputs

The application updates the UI with the current counter value via the `MessagingCenter`:

*   **UpdateUICounter:**  Sends the current counter value to the main page, allowing the UI to be updated. This is done on the main thread using `Device.BeginInvokeOnMainThread` to avoid threading issues.

Additionally, a **CancelledMessage** is sent through the `MessagingCenter` if the task is cancelled, either by the user or by the operating system.

### Usage

1.  **Start the task:** The user presses the "Start" button. This sends a `StartLongRunningTaskMessage` through the `MessagingCenter`. The platform-specific code (either Android or iOS) receives this message and starts a background service or task that runs the counter.
2.  **Update the UI:** The background task periodically increments a counter and sends the updated value to the main page using the `UpdateUICounter` message. The main page receives this message and updates a label to display the counter value.
3.  **Stop the task:** The user presses the "Stop" button, sending a `StopLongRunningTaskMessage` message, which stops the background service or task.
4.  **Cancellation:** If the task is cancelled, a `CancelledMessage` is sent to the main page.

### Implementation Details

*   The counter logic is implemented in a shared `CounterService` class that implements the `IService` interface.
*   The background task is managed by platform-specific code:
    *   **Android:** Utilizes a background service (`DroidLongRunningTaskCounter`).
    *   **iOS:** Uses a background task (`iOSLongRunningTaskCounter`).
*   The platform-specific code invokes a shared `LongrunningTimerTask` class which handles the main loop of the counter and sends updates to the UI.
*   Cancellation is handled using a `CancellationTokenSource`.

This application provides a simple example of how to run long-running tasks in Xamarin.Forms. It can be adapted for a variety of use cases where background processing is required, such as data synchronization, file uploads, or location tracking.