export interface BaseAPIResponse {
    code: string,
    message?: string,
    data?: any
}
export const CONFIG = {
    STATUS_CODE: {
        SUCCESS: 'SUCCESS',
        ERROR: 'ERROR'
    },
    PAGING_OPTION: [5, 10, 20, 50, 100],
    FORM: {
        TYPE: {
            EDIT: 'edit',
            ADD: 'add',
            VIEW: 'view'
        }
    },
    ORDER: {
        PAYMENT: {
            METHOD: {
                ONLINE: 'ONLINE',
                OFFLINE: 'OFFLINE'
            },
            STATUS: {
                UNPAID: 'UNPAID',
                PAID: 'PAID',
                CONFIRMING: 'CONFIRMING'
            }
        },
        STATUS: {
            DELIVERY: 'DELIVERY',
            PREPARED: 'PREPARED',
            TRANFERED: 'TRANFERED',
            CREATED: 'CREATED',
            CANCELED: 'CANCELED'
        }
    }
}