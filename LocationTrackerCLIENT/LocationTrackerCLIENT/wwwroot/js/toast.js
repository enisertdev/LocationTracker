const toastEl = document.getElementById("liveToast");
const toast = new bootstrap.Toast(toastEl);
function showToast(messageType, message) {
    console.log("toast calisti");
    const toastMessage = document.getElementById("toast-message");
    const msgType = document.getElementById("message-type");
    toastMessage.textContent = message;
    toastMessage.style.color = "black";
    msgType.textContent = messageType;
    toastEl.classList.remove("success-toast", "error-toast", "warning-toast");
    if (messageType === "SUCCESS") {
        toastEl.classList.add("success-toast");
    } else if (messageType === "ERROR") {
        toastEl.classList.add("error-toast");
    } else if (messageType === "WARNING") {
        toastEl.classList.add("warning-toast");
    }
    toast.show();
    setTimeout(() => {
        toast.hide();
    }, 10000);
}