import { Billing } from "./Billing";
import { Term } from "./Terms";
import { SubscriptionType } from "./SubsriptionType";

export class SubscriptionResult {
    id: string;
    accountId: string;
    subscriber: string;
    orderId: number
    billing: Billing;
    cgv: Term;
    subscriptionType: SubscriptionType;

    constructor(subscriptionType: SubscriptionType) {
        this.billing = new Billing()
        this.cgv = new Term();
        this.subscriptionType = subscriptionType

    }
}
