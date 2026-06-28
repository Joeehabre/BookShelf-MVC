// Renders the "Books by Status" doughnut chart on the dashboard.
(function () {
    const canvas = document.getElementById("statusChart");
    if (!canvas || typeof Chart === "undefined") return;

    const data = [
        Number(canvas.dataset.wanttoread) || 0,
        Number(canvas.dataset.reading) || 0,
        Number(canvas.dataset.finished) || 0
    ];

    new Chart(canvas, {
        type: "doughnut",
        data: {
            labels: ["Want to Read", "Reading", "Finished"],
            datasets: [{
                data: data,
                backgroundColor: ["#6c757d", "#ffc107", "#198754"],
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: "bottom" }
            }
        }
    });
})();
