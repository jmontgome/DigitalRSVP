import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { UtilityService } from '../../services/utility.service';
import { IsNullOrWhitespace } from '../../internal/string';

@Component({
    selector: 'app-report',
    templateUrl: './report.component.html',
    styleUrl: './report.component.css'
})
export class ReportComponent {
    private readonly _route = inject(ActivatedRoute);
    private readonly _router: Router;

    private readonly _utilityService: UtilityService;

    constructor(router: Router,
        utilityService: UtilityService
    )
    {
        this._router = router;
        this._utilityService = utilityService;
    }

    async TriggerReportSending() {
        let emailInput = document.getElementById('emailInput') as HTMLInputElement;
        let resultMessage = document.getElementById('resultMessage') as HTMLParagraphElement;
        
        if (!IsNullOrWhitespace(emailInput.value)) {
            await this._utilityService.TriggerReportSending(emailInput.value);
            resultMessage.textContent = 'A report is generating and should be sent to your email in the next 30 minutes!';
        }
        else {
            resultMessage.textContent = 'Please enter an email and try again.';
        }
    }
}