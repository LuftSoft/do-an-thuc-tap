export interface BaseAPIResponse {
    code : string,
    message? : string,
    data? : any
}
export const CONFIG = {
    STATUS_CODE : {
        SUCCESS : 'success',
        ERROR: 'error'
    }
}