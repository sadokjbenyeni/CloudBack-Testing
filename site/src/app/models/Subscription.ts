import { Billing } from "./Billing";
import { Term } from "./Terms";
import { SubscriptionType } from "./SubsriptionType";
import { Payment } from "./Payment";

export class Subscription {
    accountId: string;
    subscriber: string;
    billing: Billing;
    orderId: string
    cgv: Term;
    paymentMethodId: string;
    paymentCard: Payment;
    status: string
    type: SubscriptionType;
    activatedDate: Date
    activationMessage: string;
    validatedOrDeclinedDate: Date;
    declineMessage: string;

    constructor(subscriptionType: SubscriptionType) {
        this.billing = new Billing()
        this.cgv = new Term();
        this.type = subscriptionType

    }
}
