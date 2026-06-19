window.chessCharts = {
    renderResultsPie: function (canvasId, wins, draws, losses) {
        const ctx = document.getElementById(canvasId);
        if (window.resultsChart) window.resultsChart.destroy();

        window.resultsChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ['Wins', 'Draws', 'Losses'],
                datasets: [{
                    data: [wins, draws, losses],
                    backgroundColor: ['#198754', '#ffc107', '#dc3545'], // Bootstrap Success, Warning, Danger colors
                    borderWidth: 2,
                    borderColor: '#ffffff'
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: { position: 'bottom', labels: { padding: 20, usePointStyle: true } }
                }
            }
        });
    }
};